import { FormControlProps } from "react-bootstrap";

export interface UrlFormControlProps extends Omit<FormControlProps, "type"> {
  controlId: string;
}

export interface UrlFormControlWithDomainProps extends UrlFormControlProps {
  domain: string;
}
