import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { Dict, Result, SearchResultModel } from './Model/search-result-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: HttpClient) {}

  item: string;
  results: Array<Result>;
  itemContent: {
    Title: string;
    Body: string;
    Score: string;
    AnswerCount: number;
  };
  items: Array<any>;
  total: number;
  took: number;
  message: string;
  tag: string;
  count: number;
  query: string;
  isLoading: boolean;

  aggs: Dict;
  aggKeys: Array<string> = [];
  post: any;

  searchResults: SearchResultModel;
  suggested: any;

  ngOnInit() {
    this.item = 'item';
    this.items = new Array<any>();
    this.items.forEach(item => {
      item.item.Title = 'title';
      item.Body = 'body';
      item.Score = 'score';
      item.AnswerCount = 3;
    });
    this.message = 'message';
    this.tag = 'tag';
    this.count = 1;
    this.total = 0;

    this.search();
    // this.suggest();
  }

  search() {
    this._httpService
      .get(`/api/search/search?query=${this.query}`)
      .subscribe(values => {
        this.isLoading = false;
        this.searchResults = values as SearchResultModel;

        this.mapData();
      });
  }

  searchByCategory(tags: Array<string>) {
    const queryInfo = { query: '', categories: tags };

    this._httpService
      .get('/api/search/searchbycategory', { params: queryInfo })
      .subscribe(values => {
        this.isLoading = false;
        this.searchResults = values as SearchResultModel;
        this.mapData();
      });
  }

  suggest() {
    this._httpService
      .get(`/api/search/suggest?query=${this.query}`)
      .subscribe(values => {
        this.suggested = values;
      });
  }

  mapData() {
    this.total = this.searchResults.total;
    this.took = this.searchResults.searchMilliseconds;

    this.aggs = this.searchResults.aggregationsByTags;
    this.aggKeys = [];
    for (const key in this.aggs) {
      if (this.aggs.hasOwnProperty(key)) {
        this.aggKeys.push(key);
      }
    }

    this.results = this.searchResults.results;
  }

  getValue(key: string) {
    return this.aggs[key];
  }

  toggleFilters(tag: string) {
    this.isLoading = true;
    const index = this.aggKeys.indexOf(tag);
    const tags = [tag];

    this.searchByCategory(tags);
  }

  selectPost(id: number) {
    console.log(id);
    this.findPost(id).subscribe(response => {
      this.post = response as Result;
    });
  }

  findPost(id: number) {
    return this._httpService.get(`/api/search/get?id=${id}`);
  }
}
