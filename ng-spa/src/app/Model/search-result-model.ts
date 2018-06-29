export class SearchResultModel {
  total: number;
  page: number;
  searchMilliseconds: number;
  results: Array<Result>;
  aggregationsByTags: Map<string, number>;
}

export class Result {
  id: string;
  title: string;
  body: string;
  tags: Array<string>;
  creationDate: Date;
}
