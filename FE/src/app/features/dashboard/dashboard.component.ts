import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit, OnDestroy {
  targetDate = new Date('2025-12-31T23:59:59');
  intervalId: any;

  days: number = 0;
  hours: number = 0;
  minutes: number = 0;
  seconds: number = 0;

  cards = [
    {
      title: 'Tổng số bác sĩ',
      link: '/doctors',
      type: 'primary'
    },
    {
      title: 'Tổng số bệnh nhân',
      link: '/patients',
      type: 'success'
    },
    {
      title: 'Tổng số ca điều trị',
      link: '/patients',
      type: 'warning'
    },
    // {
    //   title: 'Tổng số phòng khám',
    //   link: '#',
    //   type: 'info'
    // }
  ];

  ngOnInit(): void {
    this.updateCountdown();
    this.intervalId = setInterval(() => this.updateCountdown(), 1000);
  }

  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }

  updateCountdown(): void {
    const now = new Date().getTime();
    const distance = this.targetDate.getTime() - now;

    this.days = Math.floor(distance / (1000 * 60 * 60 * 24));
    this.hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    this.minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    this.seconds = Math.floor((distance % (1000 * 60)) / 1000);
  }
}
