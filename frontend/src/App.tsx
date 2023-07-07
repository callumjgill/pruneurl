import { Card } from "react-bootstrap";
import "./styles/App.scss";

const App = () => {
  return (
    <Card>
      <Card.Body>
        <Card.Title>Hello, World!</Card.Title>
        <Card.Text>Testing 123, Bootstrap can you read me?</Card.Text>
      </Card.Body>
    </Card>
  );
};

export default App;
