export class LocaleFormats {
  fullLocale = 'DD.MM.YYYY HH:mm';
  dateLocale = 'DD.MM.YYYY';
  timeLocale = 'HH:mm:ss';
}

export class OwlLocaleFormats {
  parseInput = 'DD.MM.YYYY HH:mm';
  fullPickerInput = 'DD.MM.YYYY HH:mm';
  datePickerInput = 'DD.MM.YYYY';
  timePickerInput = 'HH:mm:ss';

    monthYearLabel = 'MMM YYYY';
    dateA11yLabel = 'LL';
    monthYearA11yLabel = 'MMMM YYYY';

  constructor(formats: LocaleFormats) {
    this.parseInput = formats.fullLocale;
    this.fullPickerInput = formats.fullLocale;
    this.datePickerInput = formats.dateLocale;
    this.timePickerInput = formats.timeLocale;
  }
}
