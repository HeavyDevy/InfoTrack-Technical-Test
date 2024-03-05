import { DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { SearchApi } from '../../api/search.api';
import { Subject, takeUntil } from 'rxjs';
import { SearchTableComponent } from '../search-table/search-table.component';
import { SearchFormComponent } from '../search-form/search-form.component';



@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [DecimalPipe, FormsModule, NgbTypeaheadModule, NgbPaginationModule,SearchTableComponent,SearchFormComponent],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

    searches:any;
    collectionSize:number = 5;
    pageSize:number = 5;
  
	constructor(private searchApi: SearchApi) {

	}



  

/*  this.accountsApi.getAccounts(companyId).pipe(takeUntil(this.onDestroy$)).subscribe(accounts => {
        this.accounts = accounts;

      });*/
  ngOnInit() { 
    this.refreshSearches();
    
  }

  refreshSearches() {
    this.searchApi.getSearches().subscribe(
      (response) => 
      { 
        this.searches = response; 
        this.collectionSize = response.length;
      },
      (error) => { console.log(error); });
	}

  addItem(newItem: string) {
    //this.items.push(newItem);
    this.refreshSearches();
  }

}
