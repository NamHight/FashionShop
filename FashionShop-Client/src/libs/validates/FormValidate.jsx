import {z} from "zod";

export const LoginValidate = z.object({
    email: z.string({required_error: "Email is required"}).nonempty({message: "Email is required"}).email("Invalid email"),
    password: z.string({required_error: "Password is required"}).nonempty({message: "Password is required"}),
    remember: z.boolean().optional(),
});

export const RegisterValidate = z.object({
    email: z.string()
        .nonempty({message: "Email is required"})
        .email("Invalid email"),
    password: z.string()
        .nonempty({message: "Password is required"})
        .regex(/^(?=.*[a-zA-Z])(?=.*\d).+$/,{message: "Password must contain at least one letter and one number"})
        .min(6, "Password must be at least 6 characters"),
    confirmPassword: z.string()
        .nonempty({message: "RePassword is required"})
        .min(6, "Password must be at least 6 characters"),
    customerName: z.string()
        .nonempty({message: "Fullname is required"})
        .min(5, "Fullname must be at least 5 characters")
        .max(250, "Fullname must be at most 250 characters"),
    phone: z.string()
        .nonempty({message: "Phone is required"})
        .regex(/^\d+$/,{message: "Phone number must contain only digits"})
        .regex(/^09\d{8,13}$/,{message: "Phone number must start with '09' and contain only digits"})
        .min(10, "Phone number must be at least 10 characters")
        .max(15, "Phone number must be at most 15 characters"),
}) .refine((data) => data.password === data.confirmPassword, {
    message: "Password and RePassword do not match",
    path: ["confirmPassword"], // Gán lỗi vào confirmPassword
});

export const ContactValidate = z.object({
    fullname: z.string()
        .nonempty({message: "fullName is required"}),
    email: z.string()
        .nonempty({message: "Email is required"})
        .email("Invalid email"),
    phone: z.string()
        .nonempty({message: "Phone is required"})
        .regex(/^\d+$/,{message: "Phone number must contain only digits"})
        .min(10, "Phone number must be at least 10 characters")
        .max(15, "Phone number must be at most 15 characters"),
    description: z.string()
        .nonempty({message: "Description is required"})
});