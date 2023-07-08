import { ReactNode } from "react";
import { Form, FormGroupProps, Row } from "react-bootstrap";

interface FormRowProps extends FormGroupProps {
  children?: ReactNode | ReactNode[];
}

const FormRow = (props: FormRowProps) => {
  const { children, ...formGroupProps } = { ...props };
  return (
    <Row className="mt-3 mb-3">
      <Form.Group {...formGroupProps}>{children}</Form.Group>
    </Row>
  );
};

export default FormRow;
