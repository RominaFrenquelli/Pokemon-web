using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Modelos;

namespace Pokemon_Web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Pokemon> listaPokemon { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonBusiness negocio = new PokemonBusiness();
            listaPokemon = negocio.ListarConSP();

            if (!IsPostBack)
            {
                repRepetidor.DataSource = listaPokemon;
                repRepetidor.DataBind();
            }
        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
        }
    }
}