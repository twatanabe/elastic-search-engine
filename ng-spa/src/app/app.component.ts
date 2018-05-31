import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: Http) {}

  item: string;
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


  apiValues: string[] = [];

  ngOnInit() {
    this._httpService.get('/api/core').subscribe(values => {
      this.apiValues = values.json() as string[];
    });

    this.item = 'item';
    this.items = new Array<any>();
    this.items.forEach(item => {
      item.Title = 'title';
      item.Body = 'body';
      item.Score = 'score';
      item.AnswerCount = 3;
    });
    this.total = 1;
    this.took = 1582;
    this.message = 'message';
    this.tag = 'tag';
    this.count = 1;
  }
}
