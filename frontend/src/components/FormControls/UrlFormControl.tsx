import { Form } from "react-bootstrap";
import { UrlFormControlProps } from "./UrlFormControlProps";

const UrlFormControl = (props: UrlFormControlProps) => {
  return <Form.Control {...props} type="url" />;
};

export default UrlFormControl;
