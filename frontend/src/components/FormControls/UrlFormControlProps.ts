import { FormControlProps } from "react-bootstrap";

export interface UrlFormControlProps extends Omit<FormControlProps, "type"> {
  domain: string;
}
