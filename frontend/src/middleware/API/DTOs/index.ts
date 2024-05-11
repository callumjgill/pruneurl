export interface UrlError {
  message: string;
}

export interface UrlResult {
  statusCode: number;
  prunedUrl?: string;
  error?: UrlError;
}
