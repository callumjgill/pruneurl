import { Container, Row } from "react-bootstrap";
import SimpleLink from "../links/SimpleLink";

const Footer = () => {
  return (
    <Container fluid className="footer" as="footer">
      <Row>
        <h6>
          Written by{" "}
          <SimpleLink
            linkText="@callumjgill"
            linkHref="https://github.com/callumjgill"
          />{" "}
          with <SimpleLink linkText="React" linkHref="https://react.dev/" />
        </h6>
      </Row>
      <Row>
        <h6>v{`${APP_VERSION}`}</h6>
      </Row>
    </Container>
  );
};

export default Footer;
