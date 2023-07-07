import { Button, Form, InputGroup, Row } from "react-bootstrap";

const domain = window.location.host;

const PruneUrlForm = () => {
  return (
    <Form className="form-box">
      <Row className="mb-3">
        <Form.Group controlId={`${domain}-LongURL`}>
          <Form.Label>Long URL</Form.Label>
          <Form.Control
            required
            type="url"
            placeholder="https://www.example.com/Ah73764142rrvwxcqwed1r4r"
          />
        </Form.Group>
      </Row>
      <Row className="mb-3">
        <Form.Group controlId={`${domain}-YourPrunedURL`}>
          <Form.Label>Your pruned URL</Form.Label>
          <InputGroup>
            <InputGroup.Text>{domain}/</InputGroup.Text>
            <Form.Control type="url" placeholder="example (optional)" />
          </InputGroup>
        </Form.Group>
      </Row>
      <Button type="submit">Prune</Button>
    </Form>
  );
};

export default PruneUrlForm;
