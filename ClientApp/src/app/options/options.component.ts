import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-options',
  templateUrl: './options.component.html'
})
export class OptionsComponent {
  public options: ConfigOption[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ConfigOption[]>(baseUrl + 'options').subscribe(result => {
      this.options = result;
    }, error => console.error(error));
  }
}