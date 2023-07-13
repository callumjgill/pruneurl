import API from "./API";
import PruneUrlResult from "./DTOs/PruneUrlResult";

//TODO: add togglable errors
export default class DummyApi implements API {
  private dummyPrunedUrl: string = `abc`;

  public async pruneUrl(_: string, pruneUrl?: string): Promise<PruneUrlResult> {
    await this.simulateSubmittingUrlToBackend();
    const result: PruneUrlResult = {
      prunedUrl: !pruneUrl ? this.dummyPrunedUrl : pruneUrl,
    };
    return result;
  }

  private simulateSubmittingUrlToBackend = (): Promise<void> => {
    return new Promise((resolve) => setTimeout(resolve, 2000));
  };
}
