import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html'
})
export class OptionsComponent {
  public options: Option[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Option[]>(baseUrl + 'options').subscribe(result => {
      this.options = result;
    }, error => console.error(error));
  }
}

interface Option {
  group: string;
  name: string;
  price: number;
}
