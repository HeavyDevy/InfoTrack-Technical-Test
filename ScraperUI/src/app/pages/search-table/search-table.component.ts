import { SlicePipe, DecimalPipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { SearchApi } from '../../api/search.api';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-search-table',
  standalone: true,
  imports: [DecimalPipe, FormsModule, NgbTypeaheadModule],
  templateUrl: './search-table.component.html',
  styleUrl: './search-table.component.scss'
})
export class SearchTableComponent {
  
  @Input() searches: any;    
	@Input() pageSize:number = 5;
	@Input() collectionSize:number = 5;

  HighlightRow : Number = 0;
	page = 1;
  onDestroySubject = new Subject<boolean>();
  onDestroy$ = this.onDestroySubject.asObservable();

	constructor(private searchApi: SearchApi) {

  }

	
  ngOnInit() { }

}
