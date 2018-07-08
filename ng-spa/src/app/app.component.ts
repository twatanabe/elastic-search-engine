import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {debounceTime, distinctUntilChanged, map, tap, switchMap} from 'rxjs/operators';

import { Dict, Result, SearchResultModel } from './Model/search-result-model';
import { Observable, Subscriber } from 'rxjs';

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
  queryWord: string;
  isLoading: boolean;

  aggs: Dict;
  aggKeys: Array<string> = [];
  post: any;

  searchResults: SearchResultModel;
  suggested: any;
  autoCompleteList: Array<string>;

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

  getAutoComplete = (text: Observable<string>) =>
    text.pipe(debounceTime(300), distinctUntilChanged(),
        switchMap(term => this.autoComplete(term)))

  search() {
    this._httpService
      .get(`/api/search/search?query=${this.queryWord}`)
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
      .get(`/api/search/suggest?query=${this.queryWord}`)
      .subscribe(values => {
        this.suggested = values;
      });
  }

  autoComplete(query: string) {
    if (query.length < 1) {
      return Observable.create(() => []).pipe(map(response => response));
    }

    const words = this.queryWord.split('.');
    const currentWord = words[words.length - 1];

    return this._httpService.get<string[]>('/api/search/autocomplete?q=' + currentWord).pipe(map(response => response));
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
