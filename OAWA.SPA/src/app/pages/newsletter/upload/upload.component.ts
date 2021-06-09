import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  constructor(private http: HttpClient, private formBuilder: FormBuilder,) { }
  model: any;
  apiUrl: string= 'http://localhost:5000/api/newsletter';
  newsGroupForm: FormGroup;
  ngOnInit() {
    this.newsGroupForm = this.formBuilder.group({
      name: new FormControl(""),
      subject: new FormControl(""),
      description: new FormControl(""),
    });
  }

  

  uploadNewsletters(){
    this.http.post(this.apiUrl, this.newsGroupForm).subscribe(success => {
      this.model= success;
      console.log(this.model);
    },
    error => {

    });
  }

}
