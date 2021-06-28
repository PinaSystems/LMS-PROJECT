import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss']
})
export class ViewComponent implements OnInit {

  model: any;
  apiUrl: string= 'http://localhost:5000/api/assignment';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAssignments();
  }

  getAssignments(){
    this.http.get(this.apiUrl).subscribe(success => {
      this.model= success;
      console.log(this.model);
    },
    error => {

    });
  }
}