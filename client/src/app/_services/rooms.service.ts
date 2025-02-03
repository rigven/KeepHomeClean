import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Room } from '../_models/room';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  
  getRooms() {
    return this.http.get<Room[]>(this.baseUrl + 'room');
  }

  addRoom(name: string) {
    return this.http.post<Room>(this.baseUrl + 'room', {name})
  }

  deleteRoom(id: number) {
    return this.http.delete(this.baseUrl + 'room/' + id)
  }
}
