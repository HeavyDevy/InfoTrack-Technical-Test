import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, map, take } from "rxjs";
import { environment } from "../environments/environment";
import { SearchInterface } from "../interfaces/search-interface";

@Injectable({
    providedIn: 'root'
  })
export class SearchApi {
    constructor(
        private http: HttpClient

      ) {

      }

    
      getSearches(): Observable<SearchInterface[]> {
        return this.http.get<SearchInterface[]>(`${environment.api}/Search`)
      }

      createNewSearch(keywords:string,url:string) {

        var payload = {
            keywords: keywords,
            url: url
        };

        const httpOptions = {
          headers: new HttpHeaders({'Content-Type': 'application/json'})
        }

        return this.http.post(`${environment.api}/Search`, JSON.stringify(payload), httpOptions ).pipe(
          take(1),
          catchError(async (error) => alert("Please turn on VPN and set country of origin to non-cookie consent country (ex. USA)"))
          );
      }  
}


