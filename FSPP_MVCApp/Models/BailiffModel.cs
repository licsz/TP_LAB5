using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FSPP_MVCApp.Models
{
    /// <summary>
    /// Модель данных для ФСПП (Федеральная служба судебных приставов)
    /// </summary>
    public class BailiffModel
    {
        [DisplayName("Идентификатор")]
        public int Id { get; set; }

        [DisplayName("ФИО должника")]
        public string DebtorName { get; set; }

        [DisplayName("Сумма задолженности")]
        public decimal DebtAmount { get; set; }

        [DisplayName("Дата возбуждения дела")]
        [DataType(DataType.Date)]
        public DateTime CaseOpenDate { get; set; }

        [DisplayName("Номер исполнительного производства")]
        public string CaseNumber { get; set; }

        [DisplayName("Статус исполнительного производства")]
        public string CaseStatus { get; set; }

        [DisplayName("Контактный телефон")]
        public string ContactPhone { get; set; }
    }
} 