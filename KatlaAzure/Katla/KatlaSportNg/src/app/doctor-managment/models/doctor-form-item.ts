export class DoctorFormItem {
    constructor(
        public id: number,
        public name: string,
        public surname: string,
        public departmentid: number,
        public photo: File,
    ) { }
}
