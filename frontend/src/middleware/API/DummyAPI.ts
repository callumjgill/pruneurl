import API from "./API";
import PruneUrlResult from "./DTOs/PruneUrlResult";

export default class DummyApi implements API {
  private dummyPrunedUrl: string = `abc`;

  public async pruneUrl(_: string, pruneUrl?: string): Promise<PruneUrlResult> {
    await this.simulateSubmittingUrlToBackend();
    const result: PruneUrlResult = {
      prunedUrl: !pruneUrl ? this.dummyPrunedUrl : pruneUrl,
      error: this.getError(),
    };
    return result;
  }

  private simulateSubmittingUrlToBackend = (): Promise<void> => {
    return new Promise((resolve) => setTimeout(resolve, 2000));
  };

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
