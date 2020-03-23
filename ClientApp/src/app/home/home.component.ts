import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  optionsGroups: OptionGroup[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Option[]>(baseUrl + 'options').subscribe(result => {
      let groups = {};
      this.optionsGroups = [];
      result.forEach(option => {
        if (!groups[option.group])
          groups[option.group] = [option];
        else
          groups[option.group].push(option);
      });
      for (const key in groups) {
        this.optionsGroups.push({'group':key, 'options':groups[key]});
      }
    }, error => console.error(error));
  }
}
