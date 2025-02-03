import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Duty } from '../_models/duty';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DutiesService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getDuties() {
    return this.http.get<Duty[]>(this.baseUrl + 'duties').pipe(
      map(duties => duties.map(duty => ({
          ...duty,
          lastTimeDone: new Date(duty.lastTimeDone)
      }))))
  }

  deleteDuty(dutyId: number) {
    return this.http.delete(this.baseUrl + 'duties/' + dutyId);
  }

  markDutyAsDone(dutyId: number) {
    return this.http.patch(this.baseUrl + 'duties/markAsDone/' + dutyId, {});
  }

  addDuty(duty: any) {
    return this.http.post<Duty>(this.baseUrl + 'duties', duty);
  }

  editDuty(dutyId: number, dutyModel: any) {
    return this.http.put<Duty>(this.baseUrl + 'duties/' + dutyId, dutyModel);
  }
}
