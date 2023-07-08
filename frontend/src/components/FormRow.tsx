import { ReactNode } from "react";
import { Form, FormGroupProps, Row } from "react-bootstrap";

interface FormRowProps extends FormGroupProps {
  children?: ReactNode | ReactNode[];
}

const FormRow = (props: FormRowProps) => {
  const { children, ...formGroupProps } = { ...props };
  return (
    <Row className="mb-3">
      <Form.Group {...formGroupProps}>{children}</Form.Group>
    </Row>
  );
};

export default FormRow;
