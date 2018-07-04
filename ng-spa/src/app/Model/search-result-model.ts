export class SearchResultModel {
  total: number;
  page: number;
  searchMilliseconds: number;
  results: Array<Result>;
  // aggregationsByTags: Map<string, number>;
  // aggregationsByTags: { [key: string]: number };
  // aggregationsByTags: { [key: string]: number };
  aggregationsByTags: Dict;
}

export class Result {
  id: string;
  title: string;
  body: string;
  tags: Array<string>;
  creationDate: Date;
}

export interface Dict {
  [key: string]: number;
}
