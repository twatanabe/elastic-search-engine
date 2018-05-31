import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private _httpService: Http) {}

  title = 'app';
  apiValues: string[] = [];

  ngOnInit() {
    this.title = 'Code and push';
    this._httpService.get('/api/core').subscribe(values => {
      this.apiValues = values.json() as string[];
    });
  }
}
