import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchApi } from '../../api/search.api';
import { Output, EventEmitter } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';

export interface SearchParameter {
  url: string;
  keywords: string;
}

@Component({
  selector: 'app-search-form',
  standalone: true,
  imports: [FormsModule,NzButtonModule],
  templateUrl: './search-form.component.html',
  styleUrl: './search-form.component.scss'
})
export class SearchFormComponent {
  @Output() newItemEvent = new EventEmitter<string>();

  loading:boolean = false;

  user: SearchParameter = {
    url: '',
    keywords: ''
  };

  constructor(private searchApi:SearchApi){}
 

  ngOnInit() {
    this.user = {
      url: '',
      keywords: '',
    }
  }

  save() {
    this.loading = true;
    let data = this.user.keywords + ',' + this.user.url;

    this.searchApi.createNewSearch(this.user.keywords, this.user.url).subscribe(result =>{
      this.newItemEvent.emit(data);
      this.loading = false;

    } ,error => {
        alert(`Error${JSON.stringify(error)}`);
        this.loading = false;
;  
    });   
  }
}
