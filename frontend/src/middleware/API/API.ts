import PruneUrlResult from "./DTOs/PruneUrlResult";

export default interface API {
  pruneUrl: (longUrl: string) => Promise<PruneUrlResult>;
}
