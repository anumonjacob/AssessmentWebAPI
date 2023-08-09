using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pasAPI.Data;
using pasAPI.Edit;
using pasAPI.Models;

namespace pasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasController : ControllerBase
    {
        private readonly PasDbContext _pasdbcontext;

        public PasController(PasDbContext pasdbcontext)
        {
            _pasdbcontext = pasdbcontext;
        }
        
        // Get all records of field by Type
        // GET: api/Pas/{type}
        [HttpGet]
        [Route("type/{type}")]
        public async Task<IActionResult> GetFieldByType([FromRoute] string type)
        {
            try
            {
                var fields = await _pasdbcontext.Fields.Where(x => x.Type == type).ToListAsync();
                if (fields.Count == 0)
                {
                    return NotFound($" Fields with type '{type}' was not found");
                }
                return Ok(fields);
            }
            catch 
            {
                return StatusCode(500, $"Error Occured while Getting Field of Type '{type}'");
            }
    
        }

        //Get all record from field table by form name
        // GET: api/Pas/{Form Name}
        [HttpGet]
        [Route("FormName/{FormName}")]
        public async Task<IActionResult> GetFieldByFormName([FromRoute] string FormName)
        {
            try
            {
                var forms = await _pasdbcontext.Forms.Where(f => f.Name == FormName).ToListAsync();
                if (forms.Count == 0)
                {
                    return NotFound($" Form with Name '{FormName}' was not found");
                }
                List<Field> AllFields = new List<Field>();

                foreach (var form in forms)
                {
                    var fields = await _pasdbcontext.Fields.Where(x => x.FormId == form.Id).ToListAsync();
                    
                    AllFields.AddRange(fields);
                }
                if (!AllFields.Any())
                {
                    return NotFound($" Fields with Form Name '{FormName}' was not found");
                }
                return Ok(AllFields);

            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Field with Form Name '{FormName}'");
            }

        }

        //Get all records from field table and name from form table by form Id
        // GET: api/Pas/{FormId}
        [HttpGet]
        [Route("FormId/{FormId}")]
        public async Task<ActionResult<List<Form>>> GetFieldByFormId([FromRoute] Guid FormId)
        {
            try
            {
                var form = await _pasdbcontext.Forms.FirstOrDefaultAsync(f => f.Id == FormId);
                var fields = await _pasdbcontext.Fields.Include("Form").Where(x => x.FormId == FormId).ToListAsync();
                if (form != null && fields.Any())
                {
                    var response = fields.Select(field => new
                    {
                        Field = field,
                        FormName = form.Name
                    }).ToList();

                    return Ok(response);
                }
                return NotFound($" Fields or Form with Form Id '{FormId}' was not found");
            }
            catch
            {
                return StatusCode(500, $"Error Occured while Getting Field of Form Id '{FormId}'");
            }

        }

        //Edit record in the field table by passing id
        // PUT: api/Pas/{FieldId}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch]
        [Route("Id/{FieldId}")]
        public async Task<IActionResult> UpdateFieldById([FromRoute]Guid FieldId, [FromBody] EditField updatedField)
        {
            try
            {
                var existingField = await _pasdbcontext.Fields.FindAsync(FieldId);
                
                if (existingField != null)
                {

                    existingField.FormId = updatedField.FormId ?? existingField.FormId;
                    existingField.ColumnId = updatedField.ColumnId ?? existingField.ColumnId;
                    existingField.DomainTableId = updatedField.DomainTableId ?? existingField.DomainTableId;
                    existingField.ViewResourceId = updatedField.ViewResourceId ?? existingField.ViewResourceId;
                    existingField.ModifyResourceId = updatedField.ModifyResourceId ?? existingField.ModifyResourceId;
                    existingField.AddChangeDeleteFlag = updatedField.AddChangeDeleteFlag ?? existingField.AddChangeDeleteFlag;
                    existingField.Sequence = updatedField.Sequence ?? existingField.Sequence;
                    existingField.Type = updatedField.Type ?? existingField.Type;
                    existingField.TextAreaRows = updatedField.TextAreaRows ?? existingField.TextAreaRows;
                    existingField.TextAreaCols = updatedField.TextAreaCols ?? existingField.TextAreaCols;
                    existingField.Label =  updatedField.Label ?? existingField.Label;
                    existingField.DisplayColumns = updatedField.DisplayColumns ?? existingField.DisplayColumns;
                    existingField.QuoteReadOnly = updatedField.QuoteReadOnly ?? existingField.QuoteReadOnly;
                    existingField.QuoteRequired = updatedField.QuoteRequired ?? existingField.QuoteRequired;
                    existingField.QuoteDisplay = updatedField.QuoteDisplay ?? existingField.QuoteDisplay;
                    existingField.QuoteDisabled = updatedField.QuoteDisabled ?? existingField.QuoteDisabled;
                    existingField.PolicyReadOnly = updatedField.PolicyReadOnly ?? existingField.PolicyReadOnly;
                    existingField.PolicyRequired = updatedField.PolicyRequired ?? existingField.PolicyRequired;
                    existingField.PolicyDisplay = updatedField.PolicyDisplay ?? existingField.PolicyDisplay;
                    existingField.PolicyDisabled = updatedField.PolicyDisabled ?? existingField.PolicyDisabled;
                    existingField.RequiredCondition = updatedField.RequiredCondition ?? existingField.RequiredCondition;
                    existingField.AmendablePostIssuance = updatedField.AmendablePostIssuance ?? existingField.AmendablePostIssuance;
                    existingField.AmendablePreRenewal = updatedField.AmendablePreRenewal ?? existingField.AmendablePreRenewal;
                    existingField.Default = updatedField.Default ?? existingField.Default;  
                    existingField.Minimum = updatedField.Minimum ?? existingField.Minimum;
                    existingField.Maximum = updatedField.Maximum ?? existingField.Maximum;  
                    existingField.Mask = updatedField.Mask ?? existingField.Mask;
                    existingField.Help = updatedField.Help ?? existingField.Help;
                    existingField.HelpText = updatedField.HelpText ?? existingField.HelpText;
                    existingField.DisplayController = updatedField.DisplayController ?? existingField.DisplayController;
                    existingField.Condition = updatedField.Condition ?? existingField.Condition;
                    existingField.Comment = updatedField.Comment ?? existingField.Comment;
                    existingField.DialogFileType = updatedField.DialogFileType ?? existingField.DialogFileType;
                    existingField.DialogFileName = updatedField.DialogFileName ?? existingField.DialogFileName;
                    existingField.Auditable = updatedField.Auditable ?? existingField.Auditable;
                    existingField.AuditCondition = updatedField.AuditCondition ?? existingField.AuditCondition;
                    existingField.XslValue = updatedField.XslValue ?? existingField.XslValue;
                    existingField.RefTableId = updatedField.RefTableId ?? existingField.RefTableId;
                    existingField.TextDisplaySize = updatedField.TextDisplaySize ?? existingField.TextDisplaySize;
                    existingField.LinkText = updatedField.LinkText ?? existingField.LinkText;
                    existingField.AuditViewOnly = updatedField.AuditViewOnly ?? existingField.AuditViewOnly;
                    await _pasdbcontext.SaveChangesAsync();
                    return Ok(existingField);
                }
                return NotFound($"Field with Id '{FieldId}' was not found.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(FieldId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        //Add records in Field
        // POST: api/Pas
        [HttpPost]
        [Route("Add/{FormName}")]
        public async Task<IActionResult> AddFieldByFormName([FromRoute] string FormName, [FromBody] Field NewField)
        {
         
            try
            {
                var form = await _pasdbcontext.Forms.FirstOrDefaultAsync(f => f.Name == FormName);
                if (form == null)
                {
                    return NotFound($" Form with Name '{FormName}' was not found");
                }
                if (NewField.ColumnId == null)
                {
                    return BadRequest("ColumnId is required");
                }
                var column = await _pasdbcontext.Aocolumns.FirstOrDefaultAsync(ac => ac.Id == NewField.ColumnId);
                
                if (column == null)
                {
                    return NotFound($" ColumnId '{NewField.ColumnId}' was not found");
                }

                NewField.Id = Guid.NewGuid();
                NewField.FormId = form.Id;
                NewField.ColumnId = column.Id;
                await _pasdbcontext.Fields.AddAsync(NewField);
                await _pasdbcontext.SaveChangesAsync();
                return Ok(NewField);
            }
            catch
            {
                return StatusCode(500, "Error Occured");
            }

          
        }

        //Delete record from field table by id
        // DELETE: api/Pas/5
        [HttpDelete]
        [Route("Id/{FieldId}")]
        public async Task<IActionResult> DeleteFieldById([FromRoute] Guid FieldId)
        {
            try
            {
                var field = await _pasdbcontext.Fields.FindAsync(FieldId);
                if (field != null)
                {
                    _pasdbcontext.Fields.Remove(field);
                    await _pasdbcontext.SaveChangesAsync();

                    return Ok(field);
                }
                return NotFound("Field Not Found");
            }
            catch
            {
                return StatusCode(500,"Error Occured");
            }

            
        }

        private bool FieldExists(Guid id)
        {
            return (_pasdbcontext.Fields?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
