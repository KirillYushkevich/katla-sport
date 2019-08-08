export class DepartmentItem {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public parentid: number
    ) { }
}
