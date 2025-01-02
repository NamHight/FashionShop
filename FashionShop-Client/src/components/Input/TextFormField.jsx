import React, {useId} from 'react';
import {Input, Typography} from "@material-tailwind/react";

const TextFormField = React.forwardRef(({label,error,icon,...props},ref) => {
    const id = useId();
    return (
        <div color={"default"} className={'mb-5 block space-y-1.5'}>
            <span className={'text-sm font-semibold text-emerald-400'}>{label}</span>
            <Input ref={ref} {...props} id={id} isError={error && true} color={error ? "error" : "primary"}>
                <Input.Icon >
                    {icon}
                </Input.Icon>
            </Input>
            {
                error && (
                    <Typography type={"small"} color={"error"}>
                        {error}
                    </Typography>
                )
            }
        </div>
    );
});

export default TextFormField;