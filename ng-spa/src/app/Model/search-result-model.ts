export class SearchResultModel {
  total: number;
  page: number;
  searchMilliseconds: number;
  results: Array<Result>;
}

export class Result {
  id: string;
  title: string;
  body: string;
}
