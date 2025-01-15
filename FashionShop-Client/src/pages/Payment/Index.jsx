import {Link} from "react-router"
import React, { useEffect, useRef, useState } from 'react'
import { useCartConText } from "../../context/CartContext";
import { useAuth } from "../../context/AuthContext";
import { addOrders } from "../../services/api/OrdersService";
import ModalLoginRegister from "../../components/Modal/ModalLoginRegister";
import { AlertCustom } from "../../components/Alert/Alert";
import { useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";

const Payment = () => {
    const {carts, totalMoney, removeAllCart } = useCartConText();

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
  
    const checkValidateNameOnCard =  (event) =>{
      var errorNameOnCard = document.getElementById("errorNameOnCard");
      var inputName = document.getElementById("nameOnCard")
      var name = handleNameOnCard(event); // chuyển tên ng dùng nhập thành viết hoa và bỏ dấu
      console.log(name);
      if (checkNumberOrSpecialCharacter(name)){ // kiểm tra nếu tồn tại ký tự đặc biệt hoặc số thì báo lỗi
        errorNameOnCard.hidden = false;
        errorNameOnCard.textContent = "The username on the card does not include special characters, accents and numbers";
      }else{
        inputName.value = name;
        errorNameOnCard.hidden = true;
      }
    }

    const checkValidateNumberOnCard= (event) =>{
        const numberCardElement = document.getElementById("cardNumber");
        const errorCardNumber = document.getElementById("errorCardNumber");
        const numberCard = String(event).trim();
        console.log("So the da nhap: ", numberCard);
        if(numberCard.length !=16 ){
          console.log("Chưa đủ 16 ký tự", numberCard.length);
          errorCardNumber.hidden = false;
          errorCardNumber.textContent = "Card number must have 16 characters.";
        }else{
          console.log("Đã đủ 16 ký tự");
          errorCardNumber.hidden = true;
        }
    }

    const checkExpirationYearCard= (event) =>{
      const errorExpirationYear = document.getElementById("errorExpirationYear");
      const expirationMonth = document.getElementById("expirationMonth");
      const expirationYear = document.getElementById("expirationYear");
      var expirationMonthValue = expirationMonth.value;
      console.log("thang het han la:", expirationMonthValue);
      if(+event < new Date().getFullYear() && +expirationMonthValue < new Date().getFullYear()){
        console.log("The da het han");
        errorExpirationYear.hidden = false;
        errorExpirationYear.textContent = "Your card has expired please check again";
      }else{
        errorExpirationYear.hidden = true;
      }
    }

    const checkCSVCard= (event) =>{
      const csv = document.getElementById("csv");
      const errorCSVCard = document.getElementById("errorCSVCard");
      console.log("So csv la: ", event);
      if(event.trim().length != 3){
        errorCSVCard.hidden = false;
        errorCSVCard.textContent = "Please enter 3 digits for csv";
      }else{
        errorCSVCard.hidden = true;
      }
    }

    const handleCheckName = (event) =>{
        let name = event.target.value;
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

    const handleCheckPhone = (event) =>{
      let phone = event.target.value;
      const phoneElement = document.getElementById("phone"); 
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

    const handleCheckAddress = (event) =>{
      let address = event.target.value;
      const addressElement = document.getElementById("address"); 
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

    // Hàm xử lý sau khi bấm nút thanh toán
    const handlePayment= async () =>{
      const inputCash = document.getElementById("cash");
      const inputAtm = document.getElementById("atm");
      const inputPaypal = document.getElementById("paypal");
      const inputName = document.getElementById("name").value;
      const inputPhone = document.getElementById("phone").value;
      const inputAddress = document.getElementById("address").value;
      const finalMoney = document.getElementById("finalMoney").textContent.slice(1);

      // Dữ liệu của hoá đơn và chi tiết hoá đơn gửi lên server nếu thanh toán thành công
      var data = {TotalAmount: finalMoney, OrderDate: new Date(), CustomerId:user.customerId, 
        Reciver: inputName, Address: inputAddress, Phone: inputPhone, Status: "processing", ListProductId: carts.map(item => item.productId)};

      // Xử lý hình thức thanh toán, kết quả thanh toán và chuyển hướng
      if(inputCash.checked){
        const message = 'Thanh Toan That Bai, Vui Long Thu Lai';
        var result = await addOrders({...data, PaymentMethod: "cash"});
        console.log(result);
        if(result.status === 200) {
          removeAllCart(); // thanh toán thành công thì remove all cart đi
          navigate('/account/orders/', { state: { message: 'Thanh toán thành công!' } });
        }
        else navigate(`/page404/${encodeURIComponent(message)}`, { state: { errorPayment: 'Thanh toán thất bại!' } });

      }else if(inputAtm.checked){
        const message = 'Thanh Toan That Bai, Vui Long Thu Lai';
        var result = await addOrders({...data, PaymentMethod: "credit_card"});
        if(result.status === 200) {
          removeAllCart(); // thanh toán thành công thì remove all cart đi
          navigate('/account/orders/', { state: { message: 'Thanh toán thành công!' } });
        }
        else navigate(`/page404/${encodeURIComponent(message)}`, { state: { errorPayment: 'Thanh toán thất bại!' } });

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
        <div className="min-w-screen min-h-screen bg-gray-50 py-5">
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
                        onBlur={(e)=> handleCheckName(e)}
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
                        onBlur={(e)=> handleCheckPhone(e)}
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
                        onBlur={(e)=> handleCheckAddress(e)}
                        id="address"/>
                      </div>
                    </div>
                    <div className="mt-2 text-left">
                      <span hidden  id="errorAddress" className="text-red-600 font-body"></span>
                    </div>
                  </div>

                  {/* Method payment */}
                  <div className="w-full mx-auto rounded-lg bg-white border border-gray-200 text-gray-800 font-light mb-6">
                  <div className="w-full p-3 border-b border-gray-200">
                      <label htmlFor="type2" className="flex items-center cursor-pointer">
                        <input
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

                    <div className="w-full p-3 border-b border-gray-200">
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
                      <div>
                        <div className="mb-3">
                          <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                            Name on card
                          </label>
                          <div>
                            <input
                              onBlur={(e) => checkValidateNameOnCard(e.target.value)}
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
                              onBlur={(e) => checkValidateNumberOnCard(e.target.value)}
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
                        <div className="mb-3 -mx-2 flex items-end">
                          <div className="px-2 w-1/4">
                            <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                              Expiration Month
                            </label>
                            <div>
                              <select className="form-select w-full px-3 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors cursor-pointer"
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
                                onBlur={(e) => checkExpirationYearCard(e.target.value)}
                                id="expirationYear"
                                className="w-full px-3 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                                placeholder="Ex: 2030"
                                type="text"
                              />
                          </div>
                          
                          <div className="px-2 w-1/4">
                            <label className="text-gray-600 font-semibold text-sm mb-2 ml-1">
                              Security Code
                            </label>
                            <div>
                              <input
                                onBlur={(e) => checkCSVCard(e.target.value)}
                                id = "csv"
                                className="w-full px-3 py-2 mb-1 border border-gray-200 rounded-md focus:outline-none focus:border-indigo-500 transition-colors"
                                placeholder="000"
                                type="number"
                              />
                            </div>
                          </div>
                        </div>
                        <div className="mt-2 text-left">
                            <span hidden id="errorCSVCard" className="text-red-600 font-body"></span>
                        </div>
                        <div className="mt-2 text-left">
                            <span hidden id="errorExpirationYear" className="text-red-600 font-body"></span>
                        </div>
                      </div>
                    </div>
                    <div className="w-full p-3">
                      <label htmlFor="type2" className="flex items-center cursor-pointer">
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
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        
    );
}


export default Payment