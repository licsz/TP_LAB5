using System;
using System.Web;
using System.Web.Mvc;
using FSPP_MVCApp.Models;

namespace FSPP_MVCApp.Helpers
{
    /// <summary>
    /// Внешние вспомогательные методы для работы с ФСПП
    /// </summary>
    public static class BailiffHelpers
    {
        /// <summary>
        /// Внешний вспомогательный метод для отображения статистики по исполнительному производству
        /// </summary>
        /// <param name="html">Экземпляр HtmlHelper</param>
        /// <param name="debtAmount">Сумма задолженности</param>
        /// <param name="caseStatus">Статус дела</param>
        /// <returns>Строка HTML с информацией о статистике</returns>
        public static IHtmlString CaseStatistics(this HtmlHelper html, decimal debtAmount, string caseStatus)
        {
            string cssClass = caseStatus == "Завершено" ? "completed-case" : "active-case";
            string message = GetCaseMessage(debtAmount, caseStatus);
            
            string result = $"<div class='case-stats {cssClass}'>" +
                            $"<p>Сумма задолженности: <strong>{debtAmount:C}</strong></p>" +
                            $"<p>Статус: <strong>{caseStatus}</strong></p>" +
                            $"<p>{message}</p>" +
                            $"</div>";
            
            return new HtmlString(result);
        }
        
        /// <summary>
        /// Вспомогательный метод для определения сообщения о статусе дела
        /// </summary>
        private static string GetCaseMessage(decimal debtAmount, string caseStatus)
        {
            if (caseStatus == "Завершено")
            {
                return "Дело закрыто, требования исполнены.";
            }
            else if (debtAmount > 50000)
            {
                return "Крупная задолженность. Высокий приоритет взыскания.";
            }
            else
            {
                return "Стандартное производство.";
            }
        }
    }
} 