import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  constructor(private http: HttpClient, private formBuilder: FormBuilder) { }

  model: any;
  apiUrl: string= 'http://localhost:5000/api/assignment';
  assignmentGroupForm: FormGroup;
  ngOnInit() {
    this.assignmentGroupForm = this.formBuilder.group({
      name: new FormControl(""),
      deadLine: new FormControl(""),
      description: new FormControl(""),
    });
  }

  

  uploadAssignment(){
    this.http.post(this.apiUrl, this.assignmentGroupForm).subscribe(success => {
      this.model= success;
      console.log(this.model);
    },
    error => {

    });
  }
}