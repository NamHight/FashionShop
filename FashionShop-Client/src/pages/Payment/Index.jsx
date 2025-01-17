import {Link} from "react-router"
import React, { useEffect, useRef, useState } from 'react'
import { useCartConText } from "../../context/CartContext";
import { useAuth } from "../../context/AuthContext";
import { addOrders } from "../../services/api/OrdersService";
import ModalLoginRegister from "../../components/Modal/ModalLoginRegister";
import { AlertCustom } from "../../components/Alert/Alert";
import { useNavigate } from 'react-router';
import { toast } from "react-toastify";
import { createOrder, onApprove } from "../../services/api/CartService";
import { PayPalButtons, usePayPalScriptReducer  } from '@paypal/react-paypal-js';
import { PayPalScriptProvider } from "@paypal/react-paypal-js/dist/cjs/react-paypal-js.min";



const Payment = () => {
    const {carts, totalMoney, removeAllCart, paypalClientIDx } = useCartConText();

    const {user} = useAuth();   

    const navigate = useNavigate(); // sử dụng useNavigate để chuyển hướng trang

    const [login, setLogin] = useState(false);

    const vouchers = ["Nam123", "Giam20%", "Giam10%"];

    const inputRef = useRef(null);

    useEffect(()=>{ // nếu đã đăng nhập có tồn tại user thì set state login = true hiển thị nút pay now
      if(user){
        setLogin(true);
      }
    }, [user])

    const initialOptions = {
      "client-id": paypalClientIDx,
      currency: "USD",
      intent: "capture",
    };

    const PayPalComponent = () => {
      const [{ options, isPending }, dispatch] = usePayPalScriptReducer();
     
      return (
          <div>
              {isPending && <div>Loading...</div>}
              <PayPalButtons />
          </div>
      );
  };
  
    const handleVoucher = () =>{
        var discount = document.getElementById("discount");
        const inputCoupon = inputRef.current.value;
        console.log("Gia tri da nhap trong input coupon: ", inputCoupon);
        var finalMoney = document.getElementById("finalMoney");
        var errorCoupon = document.getElementById("errorCoupon")
        var checkExist = vouchers.find(item => item == inputCoupon);
        if(checkExist){
            console.log("vao if vi san coupon co ton tai")
            errorCoupon.hidden = true;
            console.log("tong tien khi chua ap coupon: ", totalMoney());
            if(inputCoupon ===vouchers[0]){
                discount.textContent = "- $"+ (totalMoney() / 2);
                finalMoney.textContent = "$" + `${totalMoney()/2 + Math.ceil(totalMoney()/10)}`
            }else if(inputCoupon ===vouchers[1]){
                discount.textContent = "- $"+ (totalMoney() / 5);
                finalMoney.textContent = "$" + `${totalMoney()*0.8 + Math.ceil(totalMoney()/10)}`
            }else{
                discount.textContent = "- $"+ (totalMoney() / 10);
                finalMoney.textContent = "$" + `${totalMoney()*0.9 + Math.ceil(totalMoney()/10)}`
            }
        }else{
            console.log("vao else vi san coupon k ton tai")
            errorCoupon.hidden = false;
            discount.textContent = "- $0";
            finalMoney.textContent = "$" + `${totalMoney() + Math.ceil(totalMoney()/10)}`
        }
    }

    function handleNameOnCard(name) {
      var result = name.toUpperCase();
      result = result.replace(/à|ả|ã|á|ạ|ă|ằ|ẳ|ẵ|ắ|ặ|â|ầ|ẩ|ẫ|ấ|ậ/gi, 'A');
      result = result.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'I');
      result = result.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'E');
      result = result.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'O');
      result = result.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'U');
      result = result.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'Y');
      result = result.replace(/đ/gi, 'D');
      return result;
    }

    function checkNumberOrSpecialCharacter(name) {
      const regex = /[^\w\s]/gi; // biểu thức regex kiểm tra ký tự đặc biệt
      const numberRegex = /\d/gi; // biểu thức regex kiểm tra số
      return regex.test(name) || numberRegex.test(name); // nếu có chưa số hoặc ký tự đặc biệt trả về true
    }

    function checkNumberOrSpecialCharacter2(name) {
      const regex = /[^a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂỀỂỆỄÝỶỸỳỷỹĂắằẵẳẵĐđêôơưăắằẵẳẵĂÀÁẢÃÂẦẤẨẪẬẮẰẲẴẶÈÉẺẼÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ\s'.,-]/gi;
      return regex.test(name); 
    }
    
    function checkCharaterOrSpecialCharacter(number) {
      const regex = /[^\w\s]/gi; // biểu thức regex kiểm tra ký tự đặc biệt
      const letterRegex = /[a-zA-Z]/gi; // biểu thức regex kiểm tra ký chữ
      return regex.test(number) || letterRegex.test(number); // nếu có chưa số hoặc ký tự đặc biệt trả về true
    }
  
    const checkValidateNameOnCard =  () =>{
      var errorNameOnCard = document.getElementById("errorNameOnCard");
      var inputName = document.getElementById("nameOnCard");
      var name = handleNameOnCard(inputName.value); // chuyển tên ng dùng nhập thành viết hoa và bỏ dấu
      console.log(name);
      if(name.length ===0){
        errorNameOnCard.hidden = false;
        errorNameOnCard.textContent = "The cardholder's name cannot be left blank";
        return false;
      }
      else if (checkNumberOrSpecialCharacter(name)){ // kiểm tra nếu tồn tại ký tự đặc biệt hoặc số thì báo lỗi
        errorNameOnCard.hidden = false;
        errorNameOnCard.textContent = "The username on the card does not include special characters, accents and numbers";
        return false;
      }else{ // Nếu đã kiểm tra thành công thì tối ưu lại tên thành tên viết hoa, không dấu
        inputName.value = name;
        errorNameOnCard.hidden = true;
        return true;
      }
    }

    const checkValidateNumberOnCard= () =>{
        const numberCardElement = document.getElementById("cardNumber");
        const errorCardNumber = document.getElementById("errorCardNumber");
        const numberCard = String(numberCardElement.value).trim();
        console.log("So the da nhap: ", numberCard);
        if(numberCard.length ===0){
          errorCardNumber.hidden = false;
          errorCardNumber.textContent = "The card number cannot be left blank.";
          return false;
        }
        if(numberCard.length !=16 ){
          console.log("Chưa đủ 16 ký tự", numberCard.length);
          errorCardNumber.hidden = false;
          errorCardNumber.textContent = "Card number must have 16 characters.";
          return false;
        }else{
          console.log("Đã đủ 16 ký tự");
          errorCardNumber.hidden = true;
          return true;
        }
    }

    const checkExpirationYearCard= () =>{
      const errorExpirationYear = document.getElementById("errorExpirationYear");
      const expirationMonth = document.getElementById("expirationMonth");
      const expirationYear = document.getElementById("expirationYear").value;
      var expirationMonthValue = expirationMonth.value;
      console.log("thang het han la:", expirationMonthValue);
      if(expirationYear.length === 0) {
        errorExpirationYear.hidden = false;
        errorExpirationYear.textContent = "The card expiration year cannot be left blank";
        return false;
      }

      if(+expirationYear <= 0){
        errorExpirationYear.hidden = false;
        errorExpirationYear.textContent = "The expiration year must always be a positive integer";
        return false;
      }
      else if(+expirationYear < new Date().getFullYear() && +expirationMonthValue < new Date().getFullYear()){
        console.log("The da het han");
        errorExpirationYear.hidden = false;
        errorExpirationYear.textContent = "Your card has expired please check again";
        return false;
      }
      else{
        errorExpirationYear.hidden = true;
        return true;
      }
    }

    const checkCSVCard= () =>{
      const csv = document.getElementById("csv").value
      const errorCSVCard = document.getElementById("errorCSVCard");
      console.log("So csv la: ", csv);
      if(csv.length === 0) {
        errorCSVCard.hidden = false;
        errorCSVCard.textContent = "The CSV number cannot be left blank.";
        return false;
      }else if(csv.trim().length != 3){
        errorCSVCard.hidden = false;
        errorCSVCard.textContent = "Please enter only 3 digits for the CSV";
        return false;
      }else{
        errorCSVCard.hidden = true;
        return true;
      }
    }

    const handleCheckName = () =>{
        const name = document.getElementById("name").value;
        const errorName = document.getElementById("errorName"); 
        if (checkNumberOrSpecialCharacter2(name)){
          errorName.hidden = false;
          errorName.textContent = "Please enter a name without special characters or numbers.";
        }else{
          console.log("Ten la: ", name)
          if(name.length == 0){
            errorName.hidden = false;
            errorName.textContent = "Please do not leave the recipient's name blank.";
          }else if(name.length >200){
            errorName.hidden = false;
            errorName.textContent = "Please do not enter more than 200 characters.";
          }else{
            errorName.hidden = true;
          }
        }
    }

    const handleCheckPhone = () =>{
      let phone = document.getElementById("phone").value;
      const errorPhone = document.getElementById("errorPhone");
      if(checkCharaterOrSpecialCharacter(phone)){
        errorPhone.hidden = false;
        errorPhone.textContent = "Please enter a phone number without letters or special characters.";
      }else{
        console.log("Phone la: ", phone);
        if(phone.length == 0){
          errorPhone.hidden = false;
          errorPhone.textContent = "Please do not leave the recipient's phone blank.";
        }else if(phone.length >15){
          errorPhone.hidden = false;
          errorPhone.textContent = "Please do not enter more than 15 characters.";
        }else{
          errorPhone.hidden = true;
        }
      }
    }

    const handleCheckAddress = () =>{
      const address = document.getElementById("address").value;
      const errorAddress = document.getElementById("errorAddress");
      console.log("Address la: ", address)
      if(address.length == 0){
        errorAddress.hidden = false;
        errorAddress.textContent = "Please do not leave the recipient's address blank.";
      }else if(address.length >200){
        errorAddress.hidden = false;
        errorAddress.textContent = "Please do not enter more than 200 characters.";
      }else{
        errorAddress.hidden = true;
      }
    }

    // Gọi thông báo nhắc người dùng đăng nhập để tiếp tục thanh toán
    const showToastRequireLogin = () =>{  
      toast.info("Please Login To Continue Payment");
    }

    const handleClickATMPayment =  () =>{
      const infoATM = document.getElementById("infoATM");
      const inputATM = document.getElementById("atm");
      inputATM.checked = true; //tích cho atm
      console.log("Đã chọn thanh toán bang atm",  inputATM.checked);
      infoATM.hidden = false; // hiển thị info
    }

    const handleClickPaypalPayment = () =>{
      document.getElementById("paypal").checked = true; // bấm chọn paypal thì tích cho paypal 
      const infoATM = document.getElementById("infoATM");
      infoATM.hidden = true; // tích xong thì ẩn infoATM đi
    }

    const handleClickCashPayment = () =>{
      document.getElementById("cash").checked = true; // bấm chọn cash thì tích cho cash 
      const infoATM = document.getElementById("infoATM");
      infoATM.hidden = true; // ẩn thông tin atm đi
    }

     // Hàm xử lý sau khi bấm nút thanh toán
     const handlePayment= async () =>{
      const inputCash = document.getElementById("cash");
      const inputAtm = document.getElementById("atm");
      const inputPaypal = document.getElementById("paypal");
      const inputName = document.getElementById("name").value;
      const inputPhone = document.getElementById("phone").value;
      const inputAddress = document.getElementById("address").value;
      const finalMoney = document.getElementById("finalMoney").textContent.slice(1);

      //Xử lý khi người dùng chưa điền thông tin nhận hàng
      if(inputName.length ===0 ||inputPhone.length===0 || inputAddress.length===0){
          console.log("co thong tin chua dien");
          handleCheckName();
          handleCheckPhone();
          handleCheckAddress();
          toast.info("Please enter your shipping information");
          return;
      }

      // Dữ liệu của hoá đơn và chi tiết hoá đơn gửi lên server nếu thanh toán thành công
      var data = {TotalAmount: finalMoney, OrderDate: new Date(), CustomerId:user.customerId, 
        Reciver: inputName, Address: inputAddress, Phone: inputPhone, Status: "processing", ListProductId: carts.map(item => item.productId)};

      // Xử lý hình thức thanh toán, kết quả thanh toán và chuyển hướng
      if(inputCash.checked){
        console.log("Đã vào xử lý thanh toán bằng cash");
        const message = 'Thanh Toan That Bai, Vui Long Thu Lai';
        var result = await addOrders({...data, PaymentMethod: "cash"});  // gọi hàm để tạo hoá đơn cho kh
        if(result.status === 200) { // tạo hoá đơn thành công thì làm...
          removeAllCart(); // thanh toán thành công thì remove all cart đi
          navigate('/account/orders/', { state: { message: 'Thanh toán thành công!' } });
        }
        else navigate(`/page404/${encodeURIComponent(message)}`, { state: { errorPayment: 'Thanh toán thất bại!' } });

      }else if(inputAtm.checked){
        console.log("Đã vào xử lý thanh toán bằng atm");
        const message = 'Thanh Toan That Bai, Vui Long Thu Lai';
        // Kiểm tra validate thành công mới cho thanh toán, nếu không sẽ thông báo nhập lại thông tin atm
        const validYear = checkExpirationYearCard();
        const validCSV = checkCSVCard();
        const validName = checkValidateNumberOnCard();
        const validNumberCard = checkValidateNameOnCard();
        console.log("Ket qua kiem tra validate",validYear, " ", validCSV, " ", validName, " ", validNumberCard);

        if(validYear && validCSV && validName && validNumberCard){ // nếu kiểm tra thành công trả về true hết thì mới tiếp tục tạo hoá đơn thanh toán
          console.log("kiem tra thanh cong")
          var result = await addOrders({...data, PaymentMethod: "credit_card"});
          if(result.status === 200) {
            removeAllCart(); // thanh toán thành công thì remove all cart đi
            navigate('/account/orders/', { state: { message: 'Thanh toán thành công!' } });
          }
          else navigate(`/page404/${encodeURIComponent(message)}`, { state: { errorPayment: 'Thanh toán thất bại!' } });

        }else{ // nếu kiểm tra validate thất bại thì thông báo kèm dừng thanh toán
          console.log("kiem tra that bai")
          toast.error("Please ensure the card information is valid before continuing with the payment");
        }
      }else if(inputPaypal.checked){
        const message = 'Thanh Toan That Bai, Vui Long Thu Lai';
        var result = await addOrders({...data, PaymentMethod: "paypal"});
        if(result.status === 200) {
          removeAllCart(); // thanh toán thành công thì remove all cart đi
          navigate('/account/orders/', { state: { message: 'Thanh toán thành công!' } });
        }
        else navigate(`/page404/${encodeURIComponent(message)}`, { state: { errorPayment: 'Thanh toán thất bại!' } });
      }
    }

    return (
        <div className="w-screen min-h-screen bg-gray-50 py-5">
          <div className="px-5">
            <div className="mb-2 items-center">
              <h1 className="text-3xl md:text-5xl font-bold text-gray-600">Checkout</h1>
            </div>
            <div className="mb-5 text-gray-400">
              <Link to="/" className="focus:outline-none hover:underline text-gray-500">Home</Link> / 
              <Link to="/cart" className="focus:outline-none hover:underline text-gray-500">Cart</Link> / 
              <span className="text-gray-600">Checkout</span>
            </div>
          </div>
          <div className="w-full bg-white border-t border-b border-gray-200 px-5 py-10 text-gray-800">
            <div className="w-full">
              <div className="-mx-3 md:flex items-start">
                {/* detail cart and subtotal */}
                <div className="px-3 md:w-7/12 lg:pr-10">
                  <div className="w-full mx-auto text-gray-800 font-light mb-6 border-b border-gray-200 pb-6">
                    {/* start cart item */}
                    {
                        carts && carts.map((item, index) =>  
                            <div key={index} className="w-full flex items-center mb-1">
                                <div className="overflow-hidden rounded-lg w-16 h-16 bg-gray-50 border border-gray-200">
                                <img
                                    src={`assets/images/products/${item.banner}`}
                                    alt={item.productName}
                                />
                                </div>
                                <div className="flex-grow pl-3">
                                <h6 className="font-semibold uppercase text-gray-600">{item.productName}</h6>
                                <p className="text-gray-400">x {item.quantity}</p>
                                </div>
                                <div>
                                <span className="font-semibold text-gray-600 text-xl">${item.price}</span>
                                </div>
                            </div>
                          )
                    }
                    {/* end cart item */}
                  </div>
                  <div className="mb-6 pb-6 border-b border-gray-200">
                    <div className="-mx-2 flex items-end justify-end">
                      <div className="flex-grow px-2 lg:max-w-xs">
                        <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">Discount code</label>
                        <div>
                          <input
                            ref = {inputRef}
                            className="w-full px-3 py-2 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                            placeholder="XXXXXX"
                            type="text"
                          />
                        </div>
                      </div>
                      <div className="px-2">
                        <button className="block w-full max-w-xs mx-auto border border-transparent bg-gray-400 hover:bg-gray-500 focus:bg-gray-500 text-white rounded-md px-5 py-2 font-semibold"
                            id = "apply"
                            onClick={handleVoucher}
                        >
                          APPLY
                        </button>
                      </div>
                    </div>
                    <div className="mt-2 text-end">
                        <span hidden id="errorCoupon" className="text-red-600 font-body">Invalid coupon code</span>
                    </div>
                  </div>
                  <div className="mb-6 pb-6 border-b border-gray-200 text-gray-800">
                    <div className="w-full flex mb-3 items-center">
                      <div className="flex-grow">
                        <span className="text-gray-600">Subtotal</span>
                      </div>
                      <div className="pl-3">
                        <span id="subTotal" className="font-semibold">${totalMoney()}</span>
                      </div>
                    </div>
                    <div className="w-full flex items-center mb-3">
                      <div className="flex-grow">
                        <span className="text-gray-600">Taxes (GST)</span>
                      </div>
                      <div className="pl-3">
                        <span className="font-semibold">+ ${Math.ceil(totalMoney()/10)}</span>
                      </div>
                    </div>
                    <div className="w-full flex items-center">
                      <div className="flex-grow">
                        <span className="text-gray-600">Discount</span>
                      </div>
                      <div className="pl-3">
                        <span id="discount" className="font-semibold">- ${0}</span>
                      </div>
                    </div>
                  </div>
                  <div className="mb-6 pb-6 border-b border-gray-200 md:border-none text-gray-800 text-xl">
                    <div className="w-full flex items-center">
                      <div className="flex-grow">
                        <span className="text-gray-600">Total</span>
                      </div>
                      <div className="pl-3">
                        <span id="finalMoney" className="font-semibold">${totalMoney() + Math.ceil(totalMoney()/10)}</span>
                      </div>
                    </div>
                  </div>
                </div>
                {/* detail payment */}
                <div className="px-3 md:w-5/12">
                  {/* info payment */}
                  <div className="w-full mx-auto rounded-lg bg-white border border-gray-200 p-3 text-gray-800 font-light mb-6">
                    {/* //item  info*/}
                    <div className="w-full flex mb-3 items-center"> 
                      <div className="w-32">
                        <span className="text-gray-600 font-semibold">Name</span>
                      </div>
                      <div className="flex-grow pl-3">
                        <input type="text" placeholder="please enter...ex: Nam Duong" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                        onBlur={()=> handleCheckName()}
                        id="name"/>
                      </div>
                    </div>
                    <div className="my-2 text-left">
                      <span hidden  id="errorName" className="text-red-600 font-body"></span>
                    </div>
                  
                    <div className="w-full flex mb-3 items-center">
                      <div className="w-32">
                        <span className="text-gray-600 font-semibold">Phone</span>
                      </div>
                      <div className="flex-grow pl-3">
                        <input type="text" placeholder="please enter...ex: 0963982322" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                        onBlur={()=> handleCheckPhone()}
                        id="phone"/>
                      </div>
                    </div>
                    <div className="my-2 text-left">
                      <span hidden  id="errorPhone" className="text-red-600 font-body"></span>
                    </div>
                   
                    <div className="w-full flex items-center">
                      <div className="w-32">
                        <span className="text-gray-600 font-semibold">Billing Address</span>
                      </div>
                      <div className="flex-grow pl-3">
                        <input type="text" placeholder="please enter...ex: 123 George Street, Sydney, NSW 2000 Australia" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                        onBlur={()=> handleCheckAddress()}
                        id="address"/>
                      </div>
                    </div>
                    <div className="mt-2 text-left">
                      <span hidden  id="errorAddress" className="text-red-600 font-body"></span>
                    </div>
                  </div>

                  {/* Method payment */}
                  <div className="w-full mx-auto rounded-lg bg-white border border-gray-200 text-gray-800 font-light mb-6">
                  <div className="w-full p-3 border-b border-gray-200" onClick={() => handleClickCashPayment()}>
                      <label htmlFor="type2" className="flex items-center cursor-pointer" >
                        <input
                          defaultChecked={true}
                          type="radio"
                          className="form-radio h-5 w-5 text-indigo-500"
                          name="type"
                          id="cash"
                        />
                        <img
                          src="assets/images/share/icon_money.jpg"
                          width="40"
                          className="ml-3"
                          alt="Cash"
                        />
                      </label>
                    </div>

                     {/* start atm payment */}
                    <div className="w-full p-3 border-b border-gray-200" onClick={() => handleClickATMPayment()}>
                      <div className="mb-5">
                        <label htmlFor="type1" className="flex items-center cursor-pointer">
                          <input
                            type="radio"
                            className="form-radio h-5 w-5 text-indigo-500"
                            name="type"
                            id="atm"
                          />
                          <img
                            src="https://leadershipmemphis.org/wp-content/uploads/2020/08/780370.png"
                            className="h-6 ml-3"
                            alt="Payment method"
                          />
                        </label>
                      </div>
                      
                      {/* info atm */}
                      <div id="infoATM" hidden>
                        <div className="mb-3">
                          <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                            Name on card
                          </label>
                          <div>
                            <input
                              onBlur={() => checkValidateNameOnCard()}
                              id="nameOnCard"
                              className="w-full px-3 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                              placeholder="John Smith"
                              type="text"
                            />
                          </div>
                          <div className="mt-2 text-left">
                            <span hidden  id="errorNameOnCard" className="text-red-600 font-body"></span>
                          </div>
                        </div>
                        <div className="mb-3">
                          <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                            Card number
                          </label>
                          <div>
                            <input
                              onBlur={() => checkValidateNumberOnCard()}
                              id="cardNumber"
                              className="w-full px-3 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                              placeholder="0000 0000 0000 0000"
                              type="number"
                            />
                          </div>
                          <div className="mt-2 text-left">
                            <span hidden id="errorCardNumber" className="text-red-600 font-body"></span>
                          </div>
                        </div>
                        <div className="mb-3 -mx-2 flex items-end justify-between">
                          <div className="px-2 w-1/4">
                            <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                              Expiration Month
                            </label>
                            <div>
                              <select className="form-select w-full px-4 pr-5 py-[10.5px] mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors cursor-pointer"
                                id="expirationMonth"
                              >
                                <option value="01">01 - January</option>
                                <option value="02">02 - February</option>
                                <option value="03">03 - March</option>
                                <option value="04">04 - April</option>
                                <option value="05">05 - May</option>
                                <option value="06">06 - June</option>
                                <option value="07">07 - July</option>
                                <option value="08">08 - August</option>
                                <option value="09">09 - September</option>
                                <option value="10">10 - October</option>
                                <option value="11">11 - November</option>
                                <option value="12">12 - December</option>
                              </select>
                            </div>
                          </div>

                          <div className="px-2 w-1/4">
                            <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                              Expiration Year
                            </label>
                            <input
                                onBlur={() => checkExpirationYearCard()}
                                id="expirationYear"
                                className="w-full px-4 pr-5 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                                placeholder="Ex: 2030"
                                type="number"
                              />
                          </div>
                          
                          <div className="px-2 w-1/4">
                            <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                              Security Code
                            </label>
                            <div>
                              <input
                                onBlur={() => checkCSVCard()}
                                id = "csv"
                                className="w-full px-4 pr-5 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                                placeholder="Ex: 000"
                                type="number"
                              />
                            </div>
                          </div>
                        </div>
                        {/* end payment atm */}
                        <div className="mt-2 text-left">
                            <span hidden id="errorCSVCard" className="text-red-600 font-body"></span>
                        </div>
                        <div className="mt-2 text-left">
                            <span hidden id="errorExpirationYear" className="text-red-600 font-body"></span>
                        </div>
                      </div>
                    </div>

                   
                    {/* start paypal payment */}
                    <div className="w-full p-3" onClick={() => handleClickPaypalPayment()}>
                      <label htmlFor="type2" className="flex items-center cursor-pointer" >
                        <input
                          type="radio"
                          className="form-radio h-5 w-5 text-indigo-500"
                          name="type"
                          id="paypal"
                        />
                        <img
                          src="https://upload.wikimedia.org/wikipedia/commons/b/b5/PayPal.svg"
                          width="80"
                          className="ml-3"
                          alt="PayPal"
                        />
                      </label>
                    </div>
                   
                  </div>

                  {/* button payment */}
                  <div>
                    {
                      login ? <button className="block w-full max-w-xs mx-auto border border-transparent bg-indigo-500 hover:bg-indigo-600 focus:bg-indigo-600 text-white rounded-lg px-3 py-2 font-semibold"
                        onClick={handlePayment}
                        >
                          PAY NOW
                        </button> :
                        <button className="block w-full max-w-xs mx-auto border border-transparent bg-indigo-500 hover:bg-indigo-600 focus:bg-indigo-600 text-white rounded-lg px-3 py-2 font-semibold"
                         onClick={showToastRequireLogin}
                         >
                            PLEASE LOGIN
                        </button>
                    }
                     {/* <PayPalScriptProvider options={initialOptions}>
                        <PayPalComponent />
                     </PayPalScriptProvider> */}
                  </div>
                </div>
              </div>
            </div>
          </div>
          {/* <script src={`https://sandbox.paypal.com/sdk/js?client-id=${}`}></script> */}
        </div> 
    );
}


export default Payment