import isDevelopment from "../../utils/isDevelopment";
import API from "./API";
import DummyApi from "./DummyAPI";
import shouldUseDummyAPI from "./utils/shouldUseDummyAPI";

const getAPI = (): API => {
  if (isDevelopment() && shouldUseDummyAPI()) {
    return new DummyApi();
  }

  // TODO: change this to a genuine implementation once ready
  return new DummyApi();
};

export default getAPI;
