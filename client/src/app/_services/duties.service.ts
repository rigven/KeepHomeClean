import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Duty } from '../_models/duty';

@Injectable({
  providedIn: 'root'
})
export class DutiesService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getDuties() {
    return this.http.get<Duty[]>(this.baseUrl + 'duties');
  }
}
