import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-newsletter',
  templateUrl: './newsletter.component.html',
  styleUrls: ['./newsletter.component.scss']
})
export class NewsletterComponent implements OnInit {

  constructor(private http: HttpClient, private formBuilder: FormBuilder) { }
  model: any;
  apiUrl: string= 'http://localhost:5000/api/newsletter';
  newsGroupForm: FormGroup;
  ngOnInit() {
    this.newsGroupForm = this.formBuilder.group({
      name: new FormControl("", [
        Validators.required,
      ]),
      subject: new FormControl("", [
        Validators.required,
      ]),
      description: new FormControl("", Validators.required),
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
