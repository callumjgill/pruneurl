import { Form, InputGroup } from "react-bootstrap";
import { UrlFormControlProps } from "./UrlFormControlProps";
import FormRow from "../FormRow";
import InvalidUrlFeedback from "../InvalidUrlFeedback";
import useBreakpoint from "../../hooks/useBreakpoint";
import {
  breakpointExtraSmallScreen,
  breakpointSmallScreen,
} from "../../utils/breakpoints";

const ShortUrlFormControlContainer = (props: {
  children: JSX.Element | JSX.Element[];
}) => {
  const breakPoint = useBreakpoint();
  if (
    breakPoint === breakpointExtraSmallScreen ||
    breakPoint === breakpointSmallScreen
  ) {
    return <>{props.children}</>;
  }

  return <InputGroup hasValidation>{props.children}</InputGroup>;
};

const UrlFormControl = (props: UrlFormControlProps) => {
  const { domain } = { ...props };

  return (
    <FormRow controlId={`${domain}-YourPrunedURL`}>
      <Form.Label>Input a desired pruned URL</Form.Label>
      <ShortUrlFormControlContainer>
        <InputGroup.Text>{domain}/</InputGroup.Text>
        <Form.Control type="text" placeholder="example (optional)" />
        <InvalidUrlFeedback />
      </ShortUrlFormControlContainer>
    </FormRow>
  );
};

export default UrlFormControl;
