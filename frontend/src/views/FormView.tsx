import { Container, Row } from "react-bootstrap";
import PruneUrlForm from "../components/PruneUrlForm";

const FormView = () => {
  return (
    <Container className="form-flex-container">
      <Row className="form-box border border-primary rounded-4">
        <PruneUrlForm />
      </Row>
    </Container>
  );
};

export default FormView;
