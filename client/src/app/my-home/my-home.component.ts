import { Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { RoomsService } from '../_services/rooms.service';
import { Room } from '../_models/room';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-my-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './my-home.component.html',
  styleUrl: './my-home.component.scss'
})
export class MyHomeComponent implements OnInit{
  private roomsService = inject(RoomsService);

  rooms = signal<Room[] | undefined>(undefined);
  newRoomName: string = "";

  ngOnInit(): void {
    this.roomsService.getRooms().subscribe({
      next: rooms => this.rooms.set(rooms)
    })
  }

  addRoom() {
    this.roomsService.addRoom(this.newRoomName).subscribe({
      next: room => {
        this.rooms.set([...this.rooms()!, room]);
        this.newRoomName = "";
      }
    })
  }

  deleteRoom(id: number) {
    this.roomsService.deleteRoom(id).subscribe({
      next: () => {
        this.rooms.set(this.rooms()?.filter(r => r.id != id));
      }
    })
  }
}
