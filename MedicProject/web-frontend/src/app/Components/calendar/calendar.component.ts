import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  addDays,
  isSameDay,
  isSameMonth,
  parseISO,
} from 'date-fns';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarMonthViewBeforeRenderEvent,
  CalendarView,
  DAYS_OF_WEEK,
} from 'angular-calendar';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { HttpClient } from '@angular/common/http';
import { AccountService } from 'src/app/Services/account.service';
import { AddAppointmentComponent } from '../add-appointment/add-appointment.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { filter, map, take } from 'rxjs/operators';
import { start } from 'repl';

const colors: any = {
  red: {
    primary: '#B9F1DA',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};

@Component({
  selector: 'app-calendar',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  events: CalendarEvent;
  appointments: Appointment[];
  ready: boolean = true;
  excludeDays: number[] = [0, 6];
  weekStartsOn = DAYS_OF_WEEK.SUNDAY;
  eventEmitter$: Observable<CalendarEvent<any>[]>;

  ngOnInit(): void {
    this.eventEmitter$ = this.getNextApp();
  }

  openDialog(){
    const dialogRef = this.dialog.open(AddAppointmentComponent);
    dialogRef.afterClosed().subscribe(result => {

    });
  }

  getNextApp() : Observable<CalendarEvent[]>{
    return this.http.get("https://localhost:5001/api/appointments/nextAppointments",{
      headers: {
        "Authorization": "Bearer " + this.accountService.token
      }
    }).pipe(
      map((res: any) =>
        res.map((item) => {
          return {
            start: startOfDay(parseISO(item.start.toString())),
            title:  item.title + " | Hour: " + item.hour,
            end: addDays(parseISO(item.end.toString()), 0),
          }
      })
  ));
}

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  refresh: Subject<any> = new Subject();

  activeDayIsOpen: boolean = true;

  constructor(private modal: NgbModal, private http: HttpClient, private accountService: AccountService,
     private dialog: MatDialog, private cd: ChangeDetectorRef) {}

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  // eventTimesChanged({
  //   event,
  //   newStart,
  //   newEnd,
  // }: CalendarEventTimesChangedEvent): void {
  //   this.events = this.events.map((iEvent) => {
  //     if (iEvent === event) {
  //       return {
  //         ...event,
  //         start: newStart,
  //         end: newEnd,
  //       };
  //     }
  //     return iEvent;
  //   });
  //   this.handleEvent('Dropped or resized', event);
  // }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  beforeMonthViewRender(renderEvent: CalendarMonthViewBeforeRenderEvent): void {
    renderEvent.body.forEach((day) => {
      day.cssClass = 'my-cells';
    });
  }


  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}


