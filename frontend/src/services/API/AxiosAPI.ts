import {
  AxiosHeaderValue,
  AxiosInstance,
  AxiosResponse,
  isAxiosError,
} from "axios";
import API from "./API";
import { CreateShortUrlPostRequestBody, UrlError, UrlResult } from "./DTOs";

const unknownErrorStatusCode: number = 400;
const unknownErrorMessage: string = "An unknown error occurred on the client!";

export default class AxiosAPI implements API {
  private readonly axiosInstance: AxiosInstance;

  constructor(axiosInstance: AxiosInstance) {
    this.axiosInstance = axiosInstance;
  }

  public async pruneUrl(longUrl: string): Promise<UrlResult> {
    try {
      const postApiUrl: string = `${this.getBaseApiUrl()}/short-urls`;
      const requestBody: CreateShortUrlPostRequestBody = {
        longUrl,
      };
      const response: AxiosResponse = await this.axiosInstance.post(
        postApiUrl,
        requestBody,
      );
      const statusCode: number = response.status;
      const shortUrl: string | undefined = this.getShortUrl(response);
      const error: UrlError | undefined = this.getError(response);
      return {
        statusCode,
        shortUrl,
        error,
      };
    } catch (error: unknown) {
      if (!error && isAxiosError(error)) {
        return {
          statusCode: error.status ?? unknownErrorStatusCode,
          error: {
            message: error.message ?? unknownErrorMessage,
          },
        };
      }

      return {
        statusCode: unknownErrorStatusCode,
        error: {
          message: unknownErrorMessage,
        },
      };
    }
  }

  private getBaseApiUrl(): string {
    const baseApiUrl: string = import.meta.env.VITE_API_BASE_URL;
    if (!baseApiUrl) {
      throw new Error(
        "Missing environment variable 'VITE_API_BASE_URL' which should contain the base url of the REST API!",
      );
    }

    return baseApiUrl;
  }

  private getShortUrl(response: AxiosResponse): string | undefined {
    if (response.status !== 201) {
      return undefined;
    }

    // The location header contains the full url generated
    const locationHeader: AxiosHeaderValue | undefined =
      response.headers["location"];
    if (!locationHeader) {
      throw new Error(
        "Expected to find short url value in the 'location' HTTP response header but it was not found!",
      );
    }

    return locationHeader.toString();
  }

  private getError(response: AxiosResponse): UrlError | undefined {
    if (response.status === 201) {
      return undefined;
    }

    const problemDetail: string | undefined = response.data.detail;
    const message: string =
      problemDetail ? problemDetail : (
        "An unknown error or response was returned by the server!"
      );
    return {
      message,
    };
  }
}
