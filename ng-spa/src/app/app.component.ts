import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

import { Result, SearchResultModel } from './Model/search-result-model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: Http) {}

  item: string;
  results: Array<Result>;
  itemContent: {
    Title: string,
    Body: string,
    Score: string,
    AnswerCount: number
  };
  items: Array<any>;
  total: number;
  took: number;
  message: string;
  tag: string;
  count: number;
  query: string;
  aggs: Map<string, number>;


  apiValues: SearchResultModel;

  ngOnInit() {
    this.item = 'item';
    this.items = new Array<any>();
    this.items.forEach(item => {
      item.Title = 'title';
      item.Body = 'body';
      item.Score = 'score';
      item.AnswerCount = 3;
    });
    this.message = 'message';
    this.tag = 'tag';
    this.count = 1;

    this.search();
  }

  search() {
    this._httpService.get(`/api/search/search?query=${this.query}`).subscribe(values => {
      this.apiValues = values.json() as SearchResultModel;

      this.mapData();
    });
  }

  mapData() {
    this.total = this.apiValues.total;
    this.took = this.apiValues.searchMilliseconds;
    this.aggs = this.apiValues.aggregationsByTags;
    this.results = this.apiValues.results;
  }
}
