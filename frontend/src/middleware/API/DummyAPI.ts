import { waitAsync } from "../../utils/time";
import API from "./API";
import PruneUrlResult from "./DTOs/PruneUrlResult";

export default class DummyApi implements API {
  private dummyPrunedUrl: string = `abc`;

  public async pruneUrl(_: string): Promise<PruneUrlResult> {
    await waitAsync(2);
    const result: PruneUrlResult = {
      prunedUrl: this.dummyPrunedUrl,
      error: this.getError(),
    };
    return result;
  }

  private getError(): number | undefined {
    if (
      this.returnError() &&
      process.env.REACT_APP_DUMMY_API_ERRORS_STATUSCODE !== undefined
    ) {
      return Number(process.env.REACT_APP_DUMMY_API_ERRORS_STATUSCODE);
    }

    return undefined;
  }

  private returnError(): boolean {
    return (
      process.env.REACT_APP_DUMMY_API_ERRORS !== undefined &&
      process.env.REACT_APP_DUMMY_API_ERRORS === "true"
    );
  }
}
