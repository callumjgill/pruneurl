import { UrlResult } from "./DTOs";

export default interface API {
  pruneUrl: (longUrl: string) => Promise<UrlResult>;
}
