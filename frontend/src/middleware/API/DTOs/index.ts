export interface UrlError {
  message: string;
}

export interface UrlResult {
  statusCode: number;
  shortUrl?: string;
  error?: UrlError;
}

export interface CreateShortUrlPostRequestBody {
  longUrl: string;
}
