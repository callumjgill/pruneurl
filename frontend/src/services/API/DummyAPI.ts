import { waitAsync } from "../../utils/time";
import API from "./API";
import { UrlError, UrlResult } from "./DTOs";

export default class DummyApi implements API {
  private readonly dummyPrunedUrl: string = "abc";
  private readonly defaultSuccessStatusCode: number = 200;
  private readonly defaultErrorStatusCode: number = 500;
  private readonly defaultErrorMessage: string = "Internal server error!";

  public async pruneUrl(_: string): Promise<UrlResult> {
    await waitAsync(2);
    const result: UrlResult = {
      statusCode: this.getStatusCode(),
      shortUrl: this.dummyPrunedUrl,
      error: this.getError(),
    };
    return result;
  }

  private getError(): UrlError | undefined {
    const errorMessageFromEnv: string = import.meta.env
      .VITE_DUMMY_API_ERRORS_MESSAGE;
    return this.returnError() ?
        {
          message:
            errorMessageFromEnv ? errorMessageFromEnv : (
              this.defaultErrorMessage
            ),
        }
      : undefined;
  }

  private returnError(): boolean {
    return (
      import.meta.env.VITE_DUMMY_API_ERRORS !== undefined &&
      import.meta.env.VITE_DUMMY_API_ERRORS === "true"
    );
  }

  private getStatusCode(): number {
    if (!this.returnError()) {
      const successStatusCodeFromEnv: number = Number(
        import.meta.env.VITE_DUMMY_API_SUCCESS_STATUSCODE,
      );
      return !isNaN(successStatusCodeFromEnv) ?
          successStatusCodeFromEnv
        : this.defaultSuccessStatusCode;
    }

    const errorStatusCodeFromEnv: number = Number(
      import.meta.env.VITE_DUMMY_API_ERRORS_STATUSCODE,
    );
    return !isNaN(errorStatusCodeFromEnv) ?
        errorStatusCodeFromEnv
      : this.defaultErrorStatusCode;
  }
}
