import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-view-content',
  templateUrl: './view-content.component.html',
  styleUrls: ['./view-content.component.scss']
})
export class ViewContentComponent implements OnInit {

  model: any;
  apiUrl: string= 'http://localhost:5000/api/nugget';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getNuggets();
  }

  getNuggets(){
    this.http.get(this.apiUrl).subscribe(success => {
      this.model= success;
      console.log(this.model);
    },
    error => {

    });
  }
}
