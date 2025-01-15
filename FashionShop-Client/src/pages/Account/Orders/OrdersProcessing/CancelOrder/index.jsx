import {
  Dialog,
  Button,
  Typography,
  IconButton,
  Textarea,
  Radio,
} from "@material-tailwind/react";
import { useMutation } from "@tanstack/react-query";
import { useRef, useState } from "react";
import { createOrderCancel } from "../../../../../services/api/OrdersService";
import { useNavigate } from "react-router";
export const CancelOrder = ({ orderId }) => {
  const message = useRef("");
  const [reason, setReason] = useState();
  const path = useNavigate();

  const { mutate } = useMutation({
    mutationKey: ["ordercancel"],
    mutationFn: async (value) => {
      var data = await createOrderCancel(value);
      return data;
    },
    onSuccess: (response) => {
      console.log("Đã vô đây", response);
      path("/account/orders/pendingcancel");
    },
    onError: (error) => {
      console.error("Tạo mới thất bại:", error);
    },
  });

  const handleSubmit = (event) => {
    event.preventDefault();
    const formData = new FormData(event.target);
    const data = {
      orderId: formData.get("orderId"),
      reasonCancel: formData.get("reasonCancel"),
      status: "pending cancel",
    };
    mutate(data);
  };

  const handleInput = (value) => {
    message.current = value;
    setReason(message.current);
  };

  const handleValueMessage = () => {
    document.getElementById("message").classList.add("hidden");
    document.getElementById("message").value = "";
  };
  const handleShowMessage = (e) => {
    let checked = e.target.checked;
    let textDiv = document.getElementById("message");
    if (checked) {
      textDiv.classList.remove("hidden");
    } else {
      textDiv.classList.add("hidden");
    }
  };
  return (
    <Dialog size="md">
      <Dialog.Trigger
        as={Button}
        className="text-black bg-white px-7 py-3 text-xl font-serif hover:bg-red-600 hover:text-white"
        id="btn_cancel"
      >
        Cancel Order
      </Dialog.Trigger>
      <Dialog.Overlay id="popup_ordercancel">
        <Dialog.Content>
          <Dialog.DismissTrigger
            as={IconButton}
            size="sm"
            variant="ghost"
            isCircular
            color="secondary"
            className="absolute right-2 top-2"
          >
          </Dialog.DismissTrigger>
          <Typography type="h4" className="mb-5 text-center">
            Reason for order cancellation
          </Typography>
          <form onSubmit={handleSubmit} id="formCancel" className="mx-7">
            <input
              type="text"
              name="orderId"
              id="orderId"
              value={orderId}
              hidden
            />
            <input
              type="text"
              name="reasonCancel"
              id="reasonCancel"
              value={reason}
              hidden
            />
            <Radio onClick={(e) => setReason(e.target.value)}>
              <div className="flex items-center gap-2">
                <Radio.Item
                  id="01"
                  value="Đổi ý đặt hàng"
                  onClick={() => handleValueMessage()}
                >
                  <Radio.Indicator />
                </Radio.Item>
                <Typography as="label" htmlFor="01" className="text-foreground">
                  Đổi ý đặt hàng
                </Typography>
              </div>
              <div className="flex items-center gap-2">
                <Radio.Item
                  id="02"
                  value="Không muốn mua nữa"
                  onClick={() => handleValueMessage()}
                >
                  <Radio.Indicator />
                </Radio.Item>
                <Typography as="label" htmlFor="02" className="text-foreground">
                  Không muốn mua nữa
                </Typography>
              </div>
              <div className="flex items-center gap-2">
                <Radio.Item
                  id="03"
                  value="Có sản phẩm khác ok hơn"
                  onClick={() => handleValueMessage()}
                >
                  <Radio.Indicator />
                </Radio.Item>
                <Typography as="label" htmlFor="03" className="text-foreground">
                  Có sản phẩm khác ok hơn
                </Typography>
              </div>
              <div className="flex items-center gap-2">
                <Radio.Item
                  id="04"
                  value="Đặt nhầm sản phẩm"
                  onClick={() => handleValueMessage()}
                  
                >
                  <Radio.Indicator />
                </Radio.Item>
                <Typography as="label" htmlFor="04" className="text-foreground">
                  Đặt nhầm sản phẩm
                </Typography>
              </div>
              <div className="flex gap-2 w-full">
                <Radio.Item id="05" onChange={(e) => handleShowMessage(e)}>
                  <Radio.Indicator />
                </Radio.Item>
                <Typography
                  as="label"
                  htmlFor="05"
                  className="text-foreground w-full"
                >
                  <div className="w-full space-y-1.5">
                    <Typography
                      as="label"
                      htmlFor="message"
                      type="small"
                      color="default"
                      className="font-medium"
                    >
                      Lý do Khác:
                    </Typography>
                    <Textarea
                      id="message"
                      placeholder="Your message..."
                      onBlur={(e) => handleInput(e.target.value)}
                      className="hidden"
                    />
                  </div>
                </Typography>
              </div>
            </Radio>
            <div className="mt-4 flex justify-end gap-2">
              <Dialog.DismissTrigger as={Button} color="secondary">
                Cancel
              </Dialog.DismissTrigger>
              <Button
                className="px-6"
                id="btn_Send"
                disabled={reason===undefined&&true}
              >
                Send
              </Button>
            </div>
          </form>
        </Dialog.Content>
      </Dialog.Overlay>
    </Dialog>
  );
};
