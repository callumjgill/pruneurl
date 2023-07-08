import { Form, FormControlProps } from "react-bootstrap";

interface UrlFormControlProps extends Omit<FormControlProps, "type"> {
  required?: boolean;
}

const UrlFormControl = (props: UrlFormControlProps) => {
  return <Form.Control {...props} type="url" />;
};

export default UrlFormControl;
