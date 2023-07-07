import { Container, Row } from "react-bootstrap";
import PruneUrlForm from "./views/PruneUrlForm";
import "./styles/App.scss";

const App = () => {
  return (
    <Container>
      <Row>
        <PruneUrlForm />
      </Row>
    </Container>
  );
};

export default App;
